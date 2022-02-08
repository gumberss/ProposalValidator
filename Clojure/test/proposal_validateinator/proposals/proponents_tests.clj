(ns proposal-validateinator.proposals.proponents-tests
  (:require [clojure.test :refer :all]
            [schema.core :as s]
            [proposal-validateinator.proposals.proponents :as p]))

(deftest only-one-main?
  (s/with-fn-validation
    (testing "Should return as valid when has only one main proponent"
      (let [proponents [{:main true :age 1} {:main false :age 1}]]
        (is (true? (p/only-one-main? proponents))))
      (let [proponents [{:main true :age 1} {:main true :age 1}]]
        (is (false? (p/only-one-main? proponents))))
      (let [proponents [{:main false :age 1} {:main false :age 1}]]
        (is (false? (p/only-one-main? proponents)))))))

(deftest all-be-of-age?
  (s/with-fn-validation
    (testing "Should return as valid when all proponents are bo of age"
      (let [proponents [{:main true :age 20} {:main false :age 19}]]
        (is (true? (p/all-over-age? 18 proponents))))
      (let [proponents [{:main true :age 18} {:main false :age 19}]]
        (is (false? (p/all-over-age? 18 proponents))))
      (let [proponents [{:main true :age 17} {:main false :age 18}]]
        (is (false? (p/all-over-age? 18 proponents)))))))