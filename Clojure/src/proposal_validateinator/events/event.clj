(ns proposal-validateinator.events.event
  (:require [schema.core :as s]
            [clj-time.core :as t]
            [clj-time.format :as f]
            [clojure.string :as str])
  (:import (java.time LocalDate)))

(def Event
  {:id          s/Uuid
   :schema      s/Str
   :action      s/Str
   :timestamp   LocalDate
   :proposal-id s/Uuid
   :data        [s/Str]})

(s/defn parse-event :- Event
  [event :- s/Str]
  (let [[id schema action timestamp proposal-id & data] (str/split event #",")
        timestamp (f/parse (f/formatters :date-time-no-ms) timestamp)]
    {:id          id
     :schema      schema
     :action      action
     :timestamp   timestamp
     :proposal-id proposal-id
     :data        data}))
