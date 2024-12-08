import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '1m', target: 200 },
        { duration: '2m', target: 200 },
        { duration: '1m', target: 400 },
        { duration: '2m', target: 400 },
        { duration: '2m', target: 0 },
    ]
}

export default function () {
    const res = http.get('http://localhost:5274/students');

    check(res, {
        'status is 200': (r) => r.status === 200
    });

    sleep(1);
}