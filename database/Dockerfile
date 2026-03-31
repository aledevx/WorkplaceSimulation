FROM mcr.microsoft.com/mssql/server:2022-latest

USER root
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Copia toda a estrutura de pastas para o container
COPY . /usr/src/app

RUN chmod +x /usr/src/app/entrypoint.sh

CMD /bin/bash ./entrypoint.sh