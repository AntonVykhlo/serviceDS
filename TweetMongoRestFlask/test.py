from requests import get, post, put, delete
#get("http://127.0.0.1:5000/api")
data={"id": "2", "tweet":"test2", "emotion":"grumpy", "timestamp":"20180201120000"}
data2={"id": "3", "tweet":"test3", "emotion":"smile", "timestamp":"20170201120000"}
data3={"id": "4", "tweet":"test4", "emotion":"lul", "timestamp":"20150201120000"}
data4={"id": "5", "tweet":"test5", "emotion":"none", "timestamp":"20160201120000"}
data5={"id": "6", "tweet":"test6", "emotion":"sad", "timestamp":"20180101120000"}
data33={"id": "33", "tweet":"test7", "emotion":"grumpy", "timestamp":"20170101120000"}
post("http://127.0.0.1:5000/api", json=data).json()
post("http://127.0.0.1:5000/api", json=data2).json()
post("http://127.0.0.1:5000/api", json=data3).json()
post("http://127.0.0.1:5000/api", json=data4).json()
post("http://127.0.0.1:5000/api", json=data5).json()
post("http://127.0.0.1:5000/api", json=data33).json()
put("http://127.0.0.1:5000/api/1", json={"tweet": "ololo"}).json()
delete("http://127.0.0.1:5000/api/33").json()