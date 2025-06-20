#!/bin/bash
set -e

MODEL_NAME="fine-tuned-mxbai"

# Установка curl (если его нет)
if ! command -v curl &> /dev/null; then
    echo "Installing curl..."
    # Для Debian/Ubuntu
    if command -v apt &> /dev/null; then
        apt update && apt install -y curl
    # Для Alpine
    elif command -v apk &> /dev/null; then
        apk add --no-cache curl
    # Для CentOS/RHEL
    elif command -v yum &> /dev/null; then
        yum install -y curl
    else
        echo "Error: Could not install curl (unknown package manager)"
        exit 1
    fi
fi

# Запуск Ollama в фоне
ollama serve &

# Дать серверу время на запуск
sleep 5

# Проверка доступности сервера
until curl -sf http://localhost:11434/api/tags; do
    echo "Waiting for Ollama server..."
    sleep 2
done

# Создание модели (если её нет)
if ! ollama list | grep -q "$MODEL_NAME"; then
    echo "Creating model $MODEL_NAME..."
    ollama create "$MODEL_NAME" -f /Modelfile
fi

wait