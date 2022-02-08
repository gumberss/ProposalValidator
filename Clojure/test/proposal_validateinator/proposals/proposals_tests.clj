(ns proposal-validateinator.proposals.proposals-tests
  (:require [clojure.test :refer :all]
            [schema.core :as s]
            [proposal-validateinator.proposals.proposals :as p]))

(def proposal
  {:loan       {:value 0 :monthly-installments-count 0}
   :proponents []})

(deftest at-least-two-proponents?
  (s/with-fn-validation
    (testing "Should have at least two proponents in a proposal"
      (let [a-proponent {:main true :age 0}]
        (are [result proposal proponents]
          (let [proposal (assoc-in proposal [:proponents] proponents)]
            (= result (p/at-least-two-proponents? proposal)))
          true proposal [a-proponent a-proponent]
          true proposal [a-proponent a-proponent a-proponent]
          false proposal [a-proponent]
          false proposal [])))))



