(ns proposal-validateinator.proposals.loans-tests
  (:require [clojure.test :refer :all]
            [proposal-validateinator.proposals.loans :as l]))

(deftest accepted-value?
  (testing "Should return loan valid as valid when loan has a accepted value"
    (are [loan result]
      (= (l/accepted-value? loan) result)
      {:value 10 :monthly-installments-count 1} false
      {:value 29999.99 :monthly-installments-count 1} false
      {:value 30000 :monthly-installments-count 1} true
      {:value 3000000 :monthly-installments-count 1} true
      {:value 3000000.01 :monthly-installments-count 1} false)))

(deftest accepted-monthly-installments-count?
  (testing "Should return monthly installments count as valid when loan has a accepted monthly installments count"
    (are [loan result]
      (= (l/accepted-monthly-installments-count? loan) result)
      {:value 1 :monthly-installments-count 1} false
      {:value 1 :monthly-installments-count (- (* 2 12) 1)} false
      {:value 1 :monthly-installments-count (* 2 12)} true
      {:value 1 :monthly-installments-count (* 15 12)} true
      {:value 1 :monthly-installments-count (+ (* 15 12) 1)} false)))

(deftest accepted-loan?
  (testing "Should consider value and monthly installments count validator results"
    (with-redefs [l/accepted-monthly-installments-count? (fn [_loan] true)
                  l/accepted-value? (fn [_loan] true)]
      (is (true? (l/accepted-loan? {}))))
    (with-redefs [l/accepted-monthly-installments-count? (fn [_loan] false)
                  l/accepted-value? (fn [_loan] true)]
      (is (false? (l/accepted-loan? {}))))
    (with-redefs [l/accepted-monthly-installments-count? (fn [_loan] true)
                  l/accepted-value? (fn [_loan] false)]
      (is (false? (l/accepted-loan? {}))))))
