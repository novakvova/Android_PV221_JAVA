# pv221-android
Create docker hub repository - publish
```
docker build -t pv221-android-api . 
docker run -it --rm -p 5280:8080 --name pv221-android_container pv221-android-api
docker run -d --restart=always --name pv221-android_container -p 5280:8080 pv221-android-api
docker run -d --restart=always -v d:/volumes/pv221-android/uploading:/app/uploading --name pv221-android_container -p 5280:8080 pv221-android-api
docker run -d --restart=always -v /volumes/pv221-android/uploading:/app/uploading --name pv221-android_container -p 5280:8080 pv221-android-api
docker ps -a

docker stop pv221-android_container
docker rm pv221-android_container

docker images --all
docker rmi pv221-android-api

docker login
docker tag pv221-android-api:latest novakvova/pv221-android-api:latest
docker push novakvova/pv221-android-api:latest
docker pull novakvova/pv221-android-api:latest

docker ps -a
docker run -d --restart=always --name pv221-android_container -p 5280:8080 novakvova/pv221-android-api
docker run -d --restart=always -v /volumes/pv221-android/uploading:/app/uploading --name pv221-android_container -p 5280:8080 novakvova/pv221-android-api
docker pull novakvova/pv221-android-api:latest
docker images --all
docker ps -a

docker stop pv221-android_container
docker rm pv221-android_container
docker run -d --restart=always --name pv221-android_container -p 5280:8080 novakvova/pv221-android-api
```

```nginx options /etc/nginx/sites-available/default
server {
    server_name   api-pv221-android.itstep.click *.api-pv221-android.itstep.click;
    location / {
       proxy_pass         http://localhost:5280;
       proxy_http_version 1.1;
       proxy_set_header   Upgrade $http_upgrade;
       proxy_set_header   Connection keep-alive;
       proxy_set_header   Host $host;
       proxy_cache_bypass $http_upgrade;
       proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
       proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
server {
		server_name   qubix.itstep.click *.qubix.itstep.click;
		root /var/dist;
		index index.html;
		location / {
			try_files $uri /index.html;
			#try_files $uri $uri/ =404;
		}
}
sudo systemctl restart nginx
certbot
```
/var/api-qubix.itstep.click/