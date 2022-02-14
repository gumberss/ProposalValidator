(ns proposal-validateinator.events.event-tests
  (:require [clojure.test :refer :all]
            [schema.core :as s]
            [proposal-validateinator.events.event :as e]
            [clj-time.core :as t])
  (:import (java.util UUID)))

(deftest parse-event
  (s/with-fn-validation
    (testing "Should parse an event correctly"
      (let [evt "c2d06c4f-e1dc-4b2a-af61-ba15bc6d8610,proposal,created,2019-11-11T13:26:04Z,bd6abe95-7c44-41a4-92d0-edf4978c9f4e,684397.0,72"
            result (e/parse-event evt)
            expecting {:id          (UUID/fromString "c2d06c4f-e1dc-4b2a-af61-ba15bc6d8610")
                       :schema      "proposal"
                       :action      "created"
                       :timestamp   (t/date-time 2019 11 11 12 26 04 0)
                       :proposal-id (UUID/fromString "bd6abe95-7c44-41a4-92d0-edf4978c9f4e")
                       :data        ["684397.0", "72"]}]
        (= expecting result)))))