import http from 'k6/http';
import { check, sleep } from 'k6';

// 1. Configurações do Teste (Cenário)
export const options = {
  stages: [
    { duration: '20s', target: 500 },
    { duration: '1m', target: 500 },

    { duration: '10s', target: 1500 },
    { duration: '1m', target: 1500 },

    { duration: '10s', target: 3000 },
    { duration: '1m', target: 3000 },

    { duration: '60s', target: 4000 },
    { duration: '1m', target: 4000 },

    { duration: '1m', target: 0 },
  ],
};

export default function () {

  const url = 'http://host.docker.internal:5101/api/Pedido';

  const res = http.get(url);

  // Validações para garantir que a API está respondendo certo
  check(res, {
    'status é 200': (r) => r.status === 200,
    'tempo de resposta < 500ms': (r) => r.timings.duration < 500,
  });

  sleep(1);
}

//docker run --rm -i grafana/k6 run - <script.js
// { resource.service.name = "order-service" } tempo