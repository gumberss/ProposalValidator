(ns proposal-validateinator.proposals.loans-tests
  (:require [clojure.test :refer :all]
            [proposal-validateinator.proposals.loans :as l]
            [schema.core :as s]))

(def ^:private loan {:value 0 :monthly-installments-count 0})

(deftest accepted-value?
  (s/with-fn-validation
    (testing "Should return loan valid as valid when loan has a accepted value"
      (are [loan result]
        (= (l/accepted-value? loan) result)
        (assoc loan :value 10) false
        (assoc loan :value 29999.99) false
        (assoc loan :value 30000) true
        (assoc loan :value 3000000) true
        (assoc loan :value 3000000.01) false))))

(deftest accepted-monthly-installments-count?
  (s/with-fn-validation
    (testing "Should return monthly installments count as valid when loan has a accepted monthly installments count"
      (are [loan result]
        (= (l/accepted-monthly-installments-count? loan) result)
        (assoc loan :monthly-installments-count 1) false
        (assoc loan :monthly-installments-count (- (* 2 12) 1)) false
        (assoc loan :monthly-installments-count (* 2 12)) true
        (assoc loan :monthly-installments-count (* 15 12)) true
        (assoc loan :monthly-installments-count (+ (* 15 12) 1)) false))))

(deftest accepted-loan?
  (s/with-fn-validation
    (testing "Should consider value and monthly installments count validator results"
      (with-redefs [l/accepted-monthly-installments-count? (fn [_loan] true)
                    l/accepted-value? (fn [_loan] true)]
        (is (true? (l/accepted-loan? loan))))
      (with-redefs [l/accepted-monthly-installments-count? (fn [_loan] false)
                    l/accepted-value? (fn [_loan] true)]
        (is (false? (l/accepted-loan? loan))))
      (with-redefs [l/accepted-monthly-installments-count? (fn [_loan] true)
                    l/accepted-value? (fn [_loan] false)]
        (is (false? (l/accepted-loan? loan)))))))