# Build stage
FROM node:12.3.1-alpine as build
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build

# Publish stage
FROM nginx:1.17.1-alpine
MAINTAINER workflow-manager-webapp
MAINTAINER wojcikmm
EXPOSE 80
COPY --from=build /usr/src/app/dist/workflow-manager-frontend /usr/share/nginx/html

