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

(s/defn valid-main-income? :- s/Bool
  [{:keys [loan proponents]} :- Proposal]
  (let [{:keys [age income]} (p/main proponents)
        loan-value (:value loan)]
    (cond
      (> age 50) (>= income (* 2 loan-value))
      (>= age 24) (>= income (* 3 loan-value))
      true (>= income (* 4 loan-value)))))

(s/defn valid-proposal? :- s/Bool
  [proposal :- Proposal validations :- [(s/make-fn-schema s/Bool s/Any)]]
  (every? #(% proposal) validations))

(s/defn proposal-validation-chain []
  [(comp l/accepted-value? :loan)
   (comp l/accepted-monthly-installments-count? :loan)
   at-least-two-proponents?
   (comp p/only-one-main? :proponents)
   (comp p/all-over-age? :proponents)
   at-least-one-warranty?
   total-warranties-values-are-two-times-loan-value?
   (comp w/accepted-warranties-states? :warranties)])