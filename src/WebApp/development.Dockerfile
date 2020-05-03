# ----------------------------------------#
# This docker file is for development only#
# ----------------------------------------#

FROM node:12.3.1-alpine as build
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . .
EXPOSE 4200
ENTRYPOINT npm run start -- --host 0.0.0.0
