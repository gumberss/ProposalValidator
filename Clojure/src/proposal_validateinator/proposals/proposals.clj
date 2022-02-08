(ns proposal-validateinator.proposals.proposals
  (:require [proposal-validateinator.proposals.loans :as l]
            [proposal-validateinator.proposals.proponents :as p]
            [proposal-validateinator.proposals.warranties :as w]
            [schema.core :as s]))

(def Proposal
  {:loan       l/Loan
   :proponents [p/Proponent]
   :warranties [w/Warranty]})

(s/defn at-least-two-proponents? :- s/Bool
  [{:keys [proponents]} :- Proposal]
  (> (count proponents) 1))

(s/defn at-least-one-warranty? :- s/Bool
  [{:keys [warranties]} :- Proposal]
  (> (count warranties) 0))

(s/defn total-warranties-values-are-two-times-loan-value? :- s/Bool
  [{:keys [warranties loan]} :- Proposal]
  (let [warranties-total-value (->> warranties (map :value) (reduce +))
        two-times-loan (* 2 (:value loan))]
    (>= warranties-total-value two-times-loan)))

(s/defn accepted-warranties-states? :- s/Bool
  [{:keys [warranties]} :- Proposal]
  (not (some (comp #{"PR" "SC" "RS"} :fu) warranties)))
