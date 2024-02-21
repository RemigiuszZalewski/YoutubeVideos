import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import MovieForm from "../components/movies/MovieForm.tsx";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App/>,
        children: [
            {path: 'createMovie', element: <MovieForm key='create' />},
            {path: 'editMovie/:id', element: <MovieForm key='edit' />}
        ]
    }
]

export const router = createBrowserRouter(routes)