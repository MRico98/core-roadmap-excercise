import axios from 'axios';
import { UserPostRequest } from '../interfaces/requests/UserPostRequest';

const API_URL = process.env.REACT_APP_API_URL || "http://localhost:3000/api";

export const createUser = async (userData: UserPostRequest) => {
    try {
        const response = await axios.post(`${API_URL}/user`, userData);
        return response.data
    } catch (error) {
        console.error(error);
    }
};