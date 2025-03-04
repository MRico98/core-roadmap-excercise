import React from 'react';
import RegisterForm from '../components/RegisterForm';

const RegisterPage: React.FC = () => {
    return (
        <div className="register-page">
            <h1>Register</h1>
            <RegisterForm />
        </div>
    );
}

export default RegisterPage;