(ns proposal-validateinator.proposals.proponents-tests
  (:require [clojure.test :refer :all]
            [schema.core :as s]
            [proposal-validateinator.proposals.proponents :as p]))

(deftest main?
  (s/with-fn-validation
    (let [main-proponent {:main true :age 1 :income 0}]
          (testing "Should find the main proponent when its present"
            (is (= main-proponent (p/main [main-proponent {:main false :age 1 :income 0}]))))
          (testing "Should not find the main proponent when its not present"
            (is (= nil (p/main [{:main false :age 1 :income 0}]))))
          (testing "Should not find the main proponent when the proponents is empty"
            (is (= nil (p/main []))))
          (testing "Should not find the main proponent when the proponents is nil"
            (is (= nil (p/main nil)))))))

  (deftest only-one-main?
    (s/with-fn-validation
      (testing "Should return as valid when has only one main proponent"
        (let [proponents [{:main true :age 1 :income 0} {:main false :age 1 :income 0}]]
          (is (true? (p/only-one-main? proponents))))
        (let [proponents [{:main true :age 1 :income 0} {:main true :age 1 :income 0}]]
          (is (false? (p/only-one-main? proponents))))
        (let [proponents [{:main false :age 1 :income 0} {:main false :age 1 :income 0}]]
          (is (false? (p/only-one-main? proponents)))))))

  (deftest all-be-of-age?
    (s/with-fn-validation
      (testing "Should return as valid when all proponents are bo of age"
        (let [proponents [{:main true :age 20 :income 0} {:main false :age 19 :income 0}]]
          (is (true? (p/all-over-age? 18 proponents))))
        (let [proponents [{:main true :age 18 :income 0} {:main false :age 19 :income 0}]]
          (is (false? (p/all-over-age? 18 proponents))))
        (let [proponents [{:main true :age 17 :income 0} {:main false :age 18 :income 0}]]
          (is (false? (p/all-over-age? 18 proponents)))))))