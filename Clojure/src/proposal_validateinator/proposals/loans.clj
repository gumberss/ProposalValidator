(ns proposal-validateinator.proposals.loans
  (:require [schema.core :as s]))

(def Loan
  {:value                      s/Num
   :monthly-installments-count s/Int})

(s/defn accepted-value?
  [loan :- Loan]
  (>= 3000000 (:value loan) 30000))

(s/defn accepted-monthly-installments-count?
  [loan :- Loan]
  (let [two-years-months (* 2 12)
        fifteen-years-montes (* 15 12)
        loan-months (:monthly-installments-count loan)]
    (>= fifteen-years-montes loan-months two-years-months)))

(s/defn accepted-loan?
  [loan :- Loan]
  (and (accepted-value? loan)
       (accepted-monthly-installments-count? loan)))
