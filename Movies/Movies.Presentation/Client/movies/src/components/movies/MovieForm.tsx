import {NavLink, useNavigate, useParams } from "react-router-dom";
import {MovieDto} from "../../models/movieDto.ts";
import {ChangeEvent, useEffect, useState } from "react";
import apiConnector from "../../api/apiConnector.ts";
import {Button, Form, Segment } from "semantic-ui-react";

export default function MovieForm() {
    
    const {id} = useParams();
    const navigate = useNavigate();
    
    const [movie, setMovie] = useState<MovieDto>({
       id: undefined,
       title: '',
       description: '',
       createDate: undefined,
       category: '' 
    });

    useEffect(() => {
        if (id) {
            apiConnector.getMovieById(id).then(movie => setMovie(movie!))
        }
    }, [id]);
    
    function handleSubmit() {
        if (!movie.id){
            apiConnector.createMovie(movie).then(() => navigate('/'));
        } else {
            apiConnector.editMovie(movie).then(() => navigate('/'));
        }
    }
    
    function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>){
        const {name, value} = event.target;
        setMovie({...movie, [name]: value});
    }
    
    return (
        <Segment clearing inverted>
            <Form onSubmit={handleSubmit} autoComplete='off' className='ui invereted form'>
                <Form.Input placeholder='Title' name='title' value={movie.title} onChange={handleInputChange}/>
                <Form.TextArea placeholder='Description' name='description' value={movie.description} onChange={handleInputChange}/>
                <Form.Input placeholder='Category' name='category' value={movie.category} onChange={handleInputChange}/>
                <Button floated='right' positive type='submit' content='Submit'/>
                <Button as={NavLink} to='/' floated='right' type='button' content='Cancel'/>
            </Form> 
        </Segment>
    )
}