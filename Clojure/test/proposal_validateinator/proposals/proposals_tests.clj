(ns proposal-validateinator.proposals.proposals-tests
  (:require [clojure.test :refer :all]
            [schema.core :as s]
            [proposal-validateinator.proposals.proposals :as p]))

(def proposal
  {:loan       {:value 0 :monthly-installments-count 0}
   :proponents []
   :warranties [{:value 0 :fu ""}]})

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

(deftest at-least-one-warranty?
  (s/with-fn-validation
    (testing "Should have at least one warranty in a proposal"
      (let [a-warranty {:value 0 :fu ""}]
        (are [result proposal warranties]
          (let [proposal (assoc-in proposal [:warranties] warranties)]
            (= result (p/at-least-one-warranty? proposal)))
          true proposal [a-warranty]
          true proposal [a-warranty a-warranty]
          false proposal [])))))

(deftest total-warranties-values-are-two-times-loan-value?
  (s/with-fn-validation
    (testing "Should total warranties value at least two times loan value"
      (let [a-warranty {:value 100 :fu ""}]
        (are [result proposal loan-value warranties]
          (let [proposal (assoc-in proposal [:loan :value] loan-value)
                proposal (assoc-in proposal [:warranties] warranties)]
            (= result (p/total-warranties-values-are-two-times-loan-value? proposal)))
          true proposal 100 [(assoc a-warranty :value 200)]
          true proposal 100 [a-warranty a-warranty]
          true proposal 500 [a-warranty (assoc a-warranty :value 300) (assoc a-warranty :value 600)]
          true proposal 10 [(assoc a-warranty :value 21)]
          false proposal 10 [(assoc a-warranty :value 19)])))))

