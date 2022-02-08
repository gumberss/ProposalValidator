(ns proposal-validateinator.proposals.proposals
  (:require [proposal-validateinator.proposals.loans :as l]
            [proposal-validateinator.proposals.proponents :as p]
            [schema.core :as s]))

(def Proposal
  {:loan       l/Loan
   :proponents [p/Proponent]
   })


(s/defn at-least-two-proponents? :- s/Bool
  [{:keys [proponents]} :- Proposal]
  (> (count proponents) 1))