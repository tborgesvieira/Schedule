# Schedule Task e SignalR

Projeto consiste em uma demo de como utilizar o Schedule Task e SignalR no ASP.NET Core 3.1.

## Explicação

No exemplo existe um Schedule que faz um request a cada 5 segundos para um serviço na web que gera uma string dinâmica,
então após ter feito a consulta da string é enviado via SignalR a todos os clientes conectados a nova informação gerada.

