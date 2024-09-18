## Passo 1. Criar os tópicos

```
docker run --rm --network=kafka-ksqldb-sales_network confluentinc/cp-kafka:7.7.1 kafka-topics --create --topic sales --bootstrap-server kafka:9093 --partitions 1 --replication-factor 1
```

```
docker run --rm --network=kafka-ksqldb-sales_network confluentinc/cp-kafka:7.7.1 kafka-topics --create --topic products --bootstrap-server kafka:9093 --partitions 1 --replication-factor 1
```

## Passo 2. Criar as estruturas no ksqlDB

```
docker exec -it ksqldb-cli ksql http://ksqldb-server:8088
```

```
CREATE STREAM sales_stream (
    product_id STRING,
    quantity INT,
    price DOUBLE,
    sale_date STRING,  -- Alterado para STRING
    payment_method STRING,
    customer_id STRING,
    region STRING,
    discount DOUBLE,
    total_weight DOUBLE,
    delivery_date STRING  -- Alterado para STRING
) WITH (
    KAFKA_TOPIC='sales',
    VALUE_FORMAT='JSON'
);

CREATE TABLE total_sales_by_product AS
SELECT product_id,
       SUM(quantity * price) AS total_sales,
       SUM(quantity) AS total_quantity,
       AVG(price) AS avg_price,
       MIN(sale_date) AS first_sale_date,
       MAX(sale_date) AS last_sale_date
FROM sales_stream
GROUP BY product_id;
```

## Passo 3. Popular Produtos

```
docker-compose exec kafka kafka-console-producer --bootstrap-server localhost:9092 --topic sales
```

```
{"product_id": "101", "quantity": 2, "price": 10.0}
{"product_id": "102", "quantity": 1, "price": 15.0}
{"product_id": "103", "quantity": 5, "price": 8.0}
{"product_id": "104", "quantity": 2, "price": 12.0}
{"product_id": "105", "quantity": 4, "price": 20.0}
{"product_id": "106", "quantity": 1, "price": 25.0}
{"product_id": "107", "quantity": 3, "price": 18.0}
{"product_id": "108", "quantity": 6, "price": 30.0}
{"product_id": "109", "quantity": 2, "price": 22.0}
{"product_id": "110", "quantity": 1, "price": 35.0}
{"product_id": "101", "quantity": 3, "price": 10.0}
{"product_id": "102", "quantity": 2, "price": 15.0}
{"product_id": "103", "quantity": 4, "price": 8.0}
{"product_id": "104", "quantity": 5, "price": 12.0}
{"product_id": "105", "quantity": 1, "price": 20.0}
{"product_id": "106", "quantity": 2, "price": 25.0}
{"product_id": "107", "quantity": 6, "price": 18.0}
{"product_id": "108", "quantity": 3, "price": 30.0}
{"product_id": "109", "quantity": 1, "price": 22.0}
{"product_id": "110", "quantity": 4, "price": 35.0}
```

```
docker-compose exec kafka kafka-console-consumer --bootstrap-server localhost:9092 --topic products --from-beginning
```

## Passo 4. Popular Vendas

```
docker-compose exec kafka kafka-console-producer --bootstrap-server localhost:9092 --topic products
```

```
{"name": "Widget A", "category": "Widgets"}
{"name": "Widget B", "category": "Widgets"}
{"name": "Gadget X", "category": "Gadgets"}
{"name": "Gadget Y", "category": "Gadgets"}
{"name": "Tool Z", "category": "Tools"}
{"name": "Tool W", "category": "Tools"}
{"name": "Device Q", "category": "Devices"}
{"name": "Device R", "category": "Devices"}
{"name": "Instrument S", "category": "Instruments"}
{"name": "Instrument T", "category": "Instruments"}
```

```
docker-compose exec kafka kafka-console-consumer --bootstrap-server localhost:9092 --topic sales --from-beginning
```
