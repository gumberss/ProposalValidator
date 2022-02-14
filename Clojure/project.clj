(defproject proposal-validateinator "0.1.0-SNAPSHOT"
  :description "FIXME: write description"
  :url "http://example.com/FIXME"
  :license {:name "EPL-2.0 OR GPL-2.0-or-later WITH Classpath-exception-2.0"
            :url "https://www.eclipse.org/legal/epl-2.0/"}
  :dependencies [[org.clojure/clojure "1.10.1"]
                 [prismatic/schema "1.2.0"]
                 [clj-time "0.15.2"]
                 [nubank/mockfn "0.7.0"]
                 [nubank/matcher-combinators "3.3.1"]
                 [org.clojure/test.check "1.1.1"]]
  :plugins [[lein-cloverage "1.2.2"]]
  :repl-options {:init-ns proposal-validateinator.core})
