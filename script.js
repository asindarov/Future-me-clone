import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    stages: [
        { duration: '5s', target: 5 },
        { duration: '30s', target: 5 },
        { duration: '5s', target: 20 },
        { duration: '30s', target: 20 },
        { duration: '5s', target: 100 },
        { duration: '30s', target: 100 },
        { duration: '5s', target: 200 },
        { duration: '30s', target: 200 },
        { duration: '5s', target: 0 },
    ],
    thresholds: {
        http_req_duration: ['p(95)<600'],
    },
};

// export default function () {
//   http.get('http://localhost:5273/Email');
//   sleep(1);
// }


export default function () {
    const url = 'http://localhost:5273/Email/AddEmail';
    const payload = JSON.stringify({
        receiverEmail: 'test@gmail.com',
        message: 'test message 1',
        deliveryDate : "2023-02-21T14:05:40.445Z",
    });
  
    const params = {
      headers: {
        'Content-Type': 'application/json',
      },
    };
  
    http.post(url, payload, params);
  }