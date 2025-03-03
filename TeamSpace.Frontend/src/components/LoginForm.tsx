import React from 'react';
import { useForm } from 'react-hook-form';
import * as yup from 'yup';
import { LoginFormsInputs } from '../interfaces/forms/LoginFormsInputs';
import { yupResolver } from '@hookform/resolvers/yup';

const schema = yup.object().shape(
    {
        email: yup.string().email('Invalid email').required('Email is required'),
        password: yup.string().required('Password is required')
    }
);

const LoginForm: React.FC = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<LoginFormsInputs>({
        resolver: yupResolver(schema)
    });

    const onSubmit = async (data: LoginFormsInputs) => {
        try {
            console.log(data);

        } catch (error) {
            console.log(error);
        }
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div>
                <label>Email</label>
                <input type="text" {...register('email')} />
                <p>{errors.email?.message}</p>
            </div>
            <div>
                <label>Password</label>
                <input type="password" {...register('password')} />
                <p>{errors.password?.message}</p>
            </div>
            <button type="submit">Login</button>
        </form>
    );
}

export default LoginForm;