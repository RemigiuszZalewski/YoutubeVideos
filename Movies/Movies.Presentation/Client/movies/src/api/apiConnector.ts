import {MovieDto} from "../models/movieDto.ts";
import {GetMovieByIdResponse} from "../models/getMovieByIdResponse.ts";
import {PaginationRequestParams, PaginationResult} from "../models/paginationResponse.ts";
import {AxiosResponse} from "axios";
import axiosInstance from "./axiosInstance.ts";

const apiConnector = {
    
    getMovies: async (paginationRequestParams: PaginationRequestParams): Promise<PaginationResult<MovieDto[]>> => {
        const response: AxiosResponse<PaginationResult<MovieDto[]>> =
            await axiosInstance.get(`/movies?pageSize=${paginationRequestParams.pageSize}&pageNumber=${paginationRequestParams.pageNumber}`);

        if (response.data && Array.isArray(response.data.data)) {
            const modifiedData = response.data.data.map(movie => ({
                ...movie,
                createDate: movie.createDate?.slice(0, 10) ?? ""
            }));

            return {
                ...response.data,
                data: modifiedData
            };
        } else {
                return {
                    data: [],
                    paginationParams: {
                        totalItems: 0,
                        totalPages: 0,
                        currentPage: 0,
                        itemsPerPage: 0
                    }
                };
        }
    },
    
    createMovie: async (movie: MovieDto): Promise<void> => {
        await axiosInstance.post<number>(`/movies`, movie);
    },
    
    editMovie: async (movie: MovieDto) : Promise<void> => {
        await axiosInstance.put<number>(`/movies/${movie.id}`, movie);
    },
    
    deleteMovie: async (movieId: string): Promise<void> => {
        await axiosInstance.delete<number>(`/movies/${movieId}`);
        },
    
    getMovieById: async (movieId: string) : Promise<MovieDto | undefined> => {
            const response = await axiosInstance.get<GetMovieByIdResponse>(`/movies/${movieId}`);
            return response.data.movieDto;
    }
}

export default apiConnector;