#!/bin/bash
# ждем пока Kafka полностью стартует
sleep 10

# создаём топики
/opt/bitnami/kafka/bin/kafka-topics.sh --bootstrap-server kafka:9092 --create --topic raw-data --partitions 3 --replication-factor 1
/opt/bitnami/kafka/bin/kafka-topics.sh --bootstrap-server kafka:9092 --create --topic processed-data --partitions 3 --replication-factor 1
