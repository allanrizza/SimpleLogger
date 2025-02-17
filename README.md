# SimpleLogger

## 📌 Sobre o Projeto
O **SimpleLogger** é um componente leve e fácil de usar para geração de logs em arquivos `.txt` em aplicações C#. Ele permite registrar eventos e mensagens importantes durante a execução do programa, facilitando a depuração e monitoramento.

## 🚀 Funcionalidades
- 📂 Geração automática da pasta `Logs`
- 📝 Registro de logs com data e hora
- 💾 Escrita automática em arquivo `log.txt`
- 🔄 Rotação de Logs: Caso o arquivo log.txt exceda 300MB, ele é movido para um novo arquivo com o nome formatado como log_yyyy-MM-dd_HH-mm-ss.txt, baseado na data e hora atuais.

## 📦 Instalação
### 1️⃣ Adicionar o projeto ao seu código
- Clone ou baixe este repositório
- Compile o projeto para gerar a DLL (`SimpleLogger.dll`)
- Adicione a DLL como referência no seu projeto C#

### 2️⃣ Ou copie diretamente a classe `Logger.cs`
Caso não queira usar uma DLL, basta copiar o arquivo `Logger.cs` para seu projeto.

## 🛠 Como Usar
### 1️⃣ Importar o namespace:
```csharp
using SimpleLogger;
```

### 2️⃣ Registrar logs no código:
```csharp
class Program
{
    static void Main()
    {
        Logger.Log("Aplicação iniciada.");
        Logger.Log("Processando dados...");
        Logger.Log("Aplicação finalizada.");
    }
}
```

### 📝 Exemplo de saída no `Logs/log.txt`:
```
2025-02-16 10:15:30 ---> Aplicação iniciada.
2025-02-16 10:15:31 ---> Processando dados...
2025-02-16 10:15:32 ---> Aplicação finalizada.
```

### 📝 Exemplo de rotação de logs:
Caso o arquivo log.txt ultrapasse 300MB, o arquivo será movido para um novo arquivo de log, nomeado com a data e hora atuais, como por exemplo:
```
log_2025-02-16_10-15-30.txt
```

## 📝 Licença
Este projeto está licenciado sob a **MIT License**. Sinta-se à vontade para usá-lo e modificá-lo conforme necessário. Consulte o arquivo [LICENSE](./LICENSE) para mais detalhes.

---
🔹 **Autor:** Allan Gilbert Rizza
📧 Contato: allanrizza.dev@gmail.com

