import React from "react";
import * as yup from "yup";
import { RegisterFormInputs } from "../interfaces/forms/RegisterFormInputs";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { createUser } from "../services/UserServices";

const schema: yup.ObjectSchema<RegisterFormInputs> = yup.object().shape({
    userName: yup.string().required("Username is required"),
    email: yup.string().email("Invalid email").required("Email is required"),
    password: yup.string().required("Password is required"),
    confirmPassword: yup.string().oneOf([yup.ref("password")], "Passwords must match"),
    phoneNumber: yup.string().required("Phone number is required"),
});

const RegisterForm: React.FC = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<RegisterFormInputs>({
        resolver: yupResolver(schema)
    });

    const onSubmit = async (data: RegisterFormInputs) => {
        try {
            console.log(data);
            createUser({
                username: data.userName,
                email: data.email,
                password: data.password,
                phoneNumber: data.phoneNumber
            });
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div>
                <label>Username</label>
                <input type="text" {...register("userName")} />
                <p>{errors.userName?.message}</p>
            </div>        
            <div>
                <label>Email</label>
                <input type="text" {...register("email")} />
                <p>{errors.email?.message}</p>
            </div>
            <div>
                <label>Password</label>
                <input type="password" {...register("password")} />
                <p>{errors.password?.message}</p>
            </div>
            <div>
                <label>Confirm Password</label>
                <input type="password" {...register("confirmPassword")} />
                <p>{errors.confirmPassword?.message}</p>
            </div>
            <div>
                <label>Phone Number</label>
                <input type="text" {...register("phoneNumber")} />
                <p>{errors.phoneNumber?.message}</p>
            </div>
            <button type="submit">Register</button>
        </form>
    );
}

export default RegisterForm;