import { Button } from "semantic-ui-react";
import {MovieDto} from "../../models/movieDto.ts";
import apiConnector from "../../api/apiConnector.ts";
import { NavLink } from "react-router-dom";

interface Props {
    movie: MovieDto;
}

export default function MovieTableItem({movie}: Props) {
    return (
        <>
            <tr className="center aligned">
                <td data-label="Id">{movie.id}</td>
                <td data-label="Title">{movie.title}</td>
                <td data-label="Description">{movie.description}</td>
                <td data-label="CreateDate">{movie.createDate}</td>
                <td data-label="Category">{movie.category}</td>
                <td data-label="Action">
                    <Button as={NavLink} to={`editMovie/${movie.id}`} color="yellow" type="submit">
                        Edit
                    </Button>
                    <Button type="button" negative onClick={async () => {
                        await apiConnector.deleteMovie(movie.id!);
                        window.location.reload();
                    }}>
                        Delete
                    </Button>
                </td>
            </tr>
        </>
    )
}