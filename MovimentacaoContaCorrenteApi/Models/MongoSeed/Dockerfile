FROM mongo

COPY conta.json /conta.json
CMD mongoimport --host mongodbconta --db dbConta --collection conta --type json --file /conta.json --jsonArray