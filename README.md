# 🚀 Projeto de Estudo: Arquitetura com Microserviços e Filas

Este repositório marca o início de um projeto prático focado em **arquitetura de microserviços**, comunicação assíncrona e boas práticas modernas utilizando **.NET 8**.

O objetivo principal é evoluir o conhecimento em sistemas distribuídos, explorando padrões utilizados em ambientes reais de produção.

---

## 🧱 Arquitetura

O projeto segue uma abordagem **event-driven (orientada a eventos)**, onde os serviços se comunicam através de filas, garantindo baixo acoplamento e maior resiliência.

### Componentes principais:

- 🧩 **Microserviços (.NET 8)**
  - Responsáveis por regras de negócio isoladas
  - Comunicação via eventos

- 📨 **RabbitMQ**
  - Broker de mensagens para comunicação assíncrona
  - Simula o comportamento de serviços como AWS SQS/SNS

- 🗄️ **PostgreSQL**
  - Banco de dados relacional
  - Cada serviço poderá possuir seu próprio banco (Database per Service)

---

## 🔄 Fluxo inicial (alto nível)

1. Um pedido é criado via API
2. O serviço de Pedido persiste os dados
3. Um evento `PedidoCriado` é publicado na fila
4. Outros serviços consomem esse evento, como:
   - Pagamento
   - Estoque
   - Notificação

---

## 🎯 Objetivos do projeto

- Praticar arquitetura de microserviços
- Entender comunicação assíncrona com filas
- Implementar padrões como:
  - Event-driven architecture
  - Idempotência
  - Retry e Dead Letter Queue (DLQ)
- Simular cenários reais de sistemas distribuídos
- Preparar o projeto para futura migração para **AWS (SQS/SNS)**

---

## 🛠️ Tecnologias

- .NET 8
- RabbitMQ
- PostgreSQL
- Docker (em breve)

---

## 🚧 Status do Projeto

🟡 **Em fase inicial de desenvolvimento**

Este projeto está sendo construído de forma incremental, com foco em aprendizado e evolução contínua.

---

## 📌 Próximos passos

- [ ] Estruturar primeiro microserviço (Pedido)
- [ ] Configurar RabbitMQ local
- [ ] Publicação de eventos
- [ ] Implementar consumidor (Pagamento)
- [ ] Adicionar persistência com PostgreSQL
- [ ] Containerização com Docker

---

## 💡 Observação

Este projeto tem caráter educacional, com foco em explorar conceitos de arquitetura e boas práticas de mercado.

---

## 👨‍💻 Autor

Projeto desenvolvido para fins de estudo e evolução técnica em arquitetura de software.

Desenvolvido por [italo pessan] (https://www.linkedin.com/in/italopessan)
