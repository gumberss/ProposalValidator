(ns proposal-validateinator.events.event
  (:require [schema.core :as s]
            [clj-time.format :as f]
            [clojure.string :as str])
  (:import (java.util UUID)))

(def Event
  {:id          s/Uuid
   :schema      s/Str
   :action      s/Str
   :timestamp   s/Any
   :proposal-id s/Uuid
   :data        [s/Str]})

(s/defn parse-event :- Event
  [evt :- s/Str]
  (let [[id schema action timestamp proposal-id & data] (str/split evt #",")
        timestamp (f/parse (f/formatters :date-time-no-ms) timestamp)]
    {:id          (UUID/fromString id)
     :schema      schema
     :action      action
     :timestamp   timestamp
     :proposal-id (UUID/fromString proposal-id)
     :data        data}))
