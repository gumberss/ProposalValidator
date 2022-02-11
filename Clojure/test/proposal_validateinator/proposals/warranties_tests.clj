(ns proposal-validateinator.proposals.warranties-tests
  (:require [clojure.test :refer :all]
            [proposal-validateinator.proposals.warranties :as w]
            [schema.core :as s]))


(deftest accepted-warranties-states?
  (s/with-fn-validation
    (testing "Should return as valid when warranties are from a FU accepted"
      (let [sp-warranty {:value 0 :fu "SP"}
            ba-warranty {:value 0 :fu "BA"}
            sc-warranty {:value 0 :fu "SC"}
            pr-warranty {:value 0 :fu "PR"}
            rs-warranty {:value 0 :fu "RS"}]
        (are [result warranties]
            (= result (w/accepted-warranties-states? warranties))
          true [sp-warranty ba-warranty]
          true [sp-warranty]
          false [sc-warranty pr-warranty rs-warranty]
          false [rs-warranty]
          false [sc-warranty]
          false [pr-warranty])))))