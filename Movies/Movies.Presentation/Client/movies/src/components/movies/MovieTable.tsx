import React, { useState, useEffect } from 'react';
import {Container, Button, Pagination, Dropdown} from 'semantic-ui-react';
import { NavLink } from 'react-router-dom';
import MovieTableItem from './MovieTableItem';
import {PaginationRequestParams} from "../../models/paginationResponse.ts";
import {MovieDto} from "../../models/movieDto.ts";
import apiConnector from "../../api/apiConnector.ts";

export default function MovieTable() {

    const [movies, setMovies] = useState<MovieDto[]>([]);
    const [totalPageNumber, setTotalPagesNumber] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [paginationRequestParams, setPaginationRequestParams] =
        useState<PaginationRequestParams>({ pageSize: pageSize, pageNumber: currentPage });

    const fetchData = async () => {
        const paginationResult = await apiConnector.getMovies(paginationRequestParams);

        if (paginationResult.data) {
            const { data, paginationParams } = paginationResult;

        if (data && paginationParams) {
            setMovies(data);
            setTotalPagesNumber(paginationParams.totalPages);
            setCurrentPage(paginationParams.currentPage);
            setPageSize(paginationParams.itemsPerPage);
        }}
    }

    useEffect(() => {
        fetchData();
    }, [paginationRequestParams]);

    const handlePageNumberChange = (_: React.MouseEvent<HTMLAnchorElement>, data: any) => {
        setPaginationRequestParams({ ...paginationRequestParams, pageNumber: data.activePage });
    }

    const handlePageSizeChange = (_: React.SyntheticEvent, data: any) => {
        setPaginationRequestParams({ ...paginationRequestParams, pageSize: data.value});
    };

    return (
        <>
            <Container className="container-style">
                <table className="ui inverted table">
                    <thead style={{textAlign: 'center'}}>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>CreateDate</th>
                        <th>Category</th>
                        <th>Action</th>
                    </tr>
                    </thead>
                    <tbody>
                    {movies.length !== 0 && (
                        movies.map((movie, index) => (
                            <MovieTableItem key={index} movie={movie}/>
                        ))
                    )}
                    </tbody>
                </table>
                <div
                    style={{marginTop: '20px', display: 'flex', justifyContent: 'space-between', alignItems: 'center'}}>
                    <Dropdown
                        selection
                        options={[
                            {key: 5, text: '5', value: 5},
                            {key: 10, text: '10', value: 10},
                            {key: 20, text: '20', value: 20},
                            {key: 30, text: '30', value: 30},
                        ]}
                        value={pageSize}
                        onChange={handlePageSizeChange}
                    />
                    <Pagination
                        activePage={currentPage}
                        totalPages={totalPageNumber}
                        onPageChange={handlePageNumberChange}
                    />
                    <Button as={NavLink} to="createMovie" floated="right" type="button" content="Create Movie"
                            positive/>
                </div>
            </Container>
        </>
    )
}