import axios, {AxiosResponse} from 'axios';
import {API_BASE_URL} from "../../config.ts";
import {PaginationResult} from "../models/paginationResponse.ts";

const axiosInstance = axios.create({
    baseURL: `${API_BASE_URL}`
});

let isInterceptorSetup = false;

const setupResponseInterceptor = () => {
    if (!isInterceptorSetup) {
        axiosInstance.interceptors.response.use(
            (response: AxiosResponse) => {

                console.log(response);

                const paginationParams = response.headers["x-pagination"];

                if (paginationParams) {
                    response.data = new PaginationResult(response.data, JSON.parse(paginationParams));
                    return response as AxiosResponse<PaginationResult<any>>;
                }
                return response;
            },
            (error) => {
                if (error.response) {
                    const statusCode = error.response.status;
                    const data = error.response.data;

                    switch (statusCode) {
                        case 400:
                            if (data.errors) {
                                const modalStateErrors = [];

                                for (const errorItem of data.errors) {
                                    const property = errorItem.property;
                                    const errorMessage = errorItem.errorMessage;

                                    if (property && errorMessage) {
                                        modalStateErrors.push({ property, errorMessage });
                                    }
                                }

                                console.log(modalStateErrors);
                            }
                            break;
                        case 401:
                            console.log("Unauthorized access");
                            break;
                        case 403:
                            console.log("Forbidden access");
                            break;
                        case 404:
                            console.log("Not found");
                            break;
                        default:
                            console.log("Generic error");
                            break;
                    }
                }

                return Promise.reject(error);
            }
        );

        isInterceptorSetup = true;
    }
};

setupResponseInterceptor();

export default axiosInstance;