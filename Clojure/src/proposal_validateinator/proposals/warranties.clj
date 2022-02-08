(ns proposal-validateinator.proposals.warranties
  (:require [schema.core :as s]))

(def Warranty
  {:value s/Num
   :fu s/Str})
