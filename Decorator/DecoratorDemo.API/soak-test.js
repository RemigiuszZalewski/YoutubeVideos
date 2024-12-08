import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '2m', target: 100 },
        { duration: '3h', target: 100 },
        { duration: '2m', target: 0 }
    ]
}

export default function () {
    const res = http.get('http://localhost:5274/students');

    check(res, {
        'status is 200': (r) => r.status === 200,
        'response time < 500ms': (r) => r.timings.duration < 500
    });

    sleep(1);
}