(ns proposal-validateinator.proposals.warranties
  (:require [schema.core :as s]))

(def Warranty
  {:value s/Num
   :fu s/Str})

(s/defn accepted-warranties-states? :- s/Bool
  [warranties :- [Warranty]]
  (not (some (comp #{"PR" "SC" "RS"} :fu) warranties)))