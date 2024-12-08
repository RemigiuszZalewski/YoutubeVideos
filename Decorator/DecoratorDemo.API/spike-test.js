import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 100 },
        { duration: '1m', target: 100 },
        { duration: '10s', target: 300 },
        { duration: '1m', target: 300 },
        { duration: '10s', target: 100 },
        { duration: '1m', target: 100 },
        { duration: '10s', target: 0 },
    ]
}

export default function () {
    const res = http.get('http://localhost:5274/students');

    check(res, {
        'status is 200': (r) => r.status === 200
    });

    sleep(1);
}