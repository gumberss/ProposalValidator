(ns proposal-validateinator.proposals.proposals-tests
  (:require [clojure.test :refer :all]
            [schema.core :as s]
            [matcher-combinators.standalone :refer [match?]]
            [proposal-validateinator.proposals.warranties :as w]
            [proposal-validateinator.proposals.loans :as l]
            [proposal-validateinator.proposals.proposals :as p]
            [clojure.test.check.generators :as gen]
            [proposal-validateinator.proposals.proponents :as pn]))

(def proposal
  {:loan       {:value 0 :monthly-installments-count 0}
   :proponents []
   :warranties [{:value 0 :fu ""}]})

(deftest at-least-two-proponents?
  (s/with-fn-validation
    (testing "Should have at least two proponents in a proposal"
      (let [a-proponent {:main true :age 0 :income 0}]
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


(deftest valid-main-income?
  (s/with-fn-validation
    (testing "Should validate the main proponent income"
      (are [result proponent-age proponent-income loan-value]
        (let [main-proponent {:main true :age proponent-age :income proponent-income}
              proposal (assoc-in proposal [:proponents] [main-proponent])
              proposal (assoc-in proposal [:loan :value] loan-value)]
          (with-redefs [pn/main (fn [_] main-proponent)]
            (= result (p/valid-main-income? proposal))))
        false 18 999.99 250
        true 18 1000 250
        false 23 999.99 250
        false 24 999.98 333.33
        true 24 999.99 333.33
        false 49 1000 500
        false 50 1000 500
        true 51 1000 500
        true 51 1500 500))))

(deftest valid-proposal?
  (s/with-fn-validation
    (testing "Should validate proposals based on validations passed as parameters"
      (let [validation-fn (fn [_] true)]
        (is (match? true (p/valid-proposal? proposal [validation-fn]))))
      (let [validation-true-fn (fn [_] true)
            validation-false-fn (fn [_] false)]
        (is (match? false (p/valid-proposal? proposal [validation-true-fn validation-false-fn])))))))

(deftest proposal-validation-chain
  (s/with-fn-validation
    (testing "Should validate if the chain is correct"
      (= (comp l/accepted-value? :loan)
         (comp l/accepted-monthly-installments-count? :loan)
         at-least-two-proponents?
         (comp pn/only-one-main? :proponents)
         (comp pn/all-over-age? :proponents)
         at-least-one-warranty?
         total-warranties-values-are-two-times-loan-value?
         (comp w/accepted-warranties-states? :warranties) (p/proposal-validation-chain)))))