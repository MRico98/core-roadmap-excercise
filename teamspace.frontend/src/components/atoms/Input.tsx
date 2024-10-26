import React from 'react';

interface InputProps {
    type?: 'text' | 'password' | 'email' | 'number';
    value: string;
    placeholder?: string;
    onChange: (value: string) => void;
    disabled?: boolean;
    className?: string;
    id?: string;
}

const Input: React.FC<InputProps> = ({ type = 'text', value, placeholder, onChange, disabled = false, className, id }) => {
    return (
        <input
            type={type}
            value={value}
            placeholder={placeholder}
            onChange={(e) => onChange(e.target.value)}
            disabled={disabled}
            className={className}
            id={id}
        />
    );
};

export default Input;