# Kafka tutorial

## Run locally the kafka cluster
```powershell
zookeeper-server-start C:\kafka\config\zookeeper.properties
kafka-server-start C:\kafka\config\server.properties
```

## Run basic consumer

```powershell
kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic my_topic --group java-application --from-beginning
```

## Run basic producer

```powershell
kafka-console-producer.bat --bootstrap-server localhost:9092 --topic my_topic
```