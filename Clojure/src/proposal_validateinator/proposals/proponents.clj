(ns proposal-validateinator.proposals.proponents
  (:require [schema.core :as s]))

(def Proponent
  {:main s/Bool
   :age  s/Num})

(s/defn only-one-main? :- s/Bool
  [proponents :- [Proponent]]
  (->> proponents
       (filter :main)
       count
       (= 1)))

(s/defn all-over-age? :- s/Bool
  [age :- s/Int  proponents :- [Proponent]]
  (every? #(> (:age %) age) proponents))