import React from "react";
import Label from "../atoms/Label";
import Input from "../atoms/Input";

interface FormFieldProps {
  label: string;
  type?: "text" | "password" | "email" | "number";
  value: string;
  placeholder?: string;
  onChange: (value: string) => void;
  disabled?: boolean;
  className?: string;
  id?: string;
}

const FormField: React.FC<FormFieldProps> = ({ label, type = "text", value, placeholder, onChange, disabled = false, className, id }) => {
  return (
    <div className={className}>
      <Label text={label} htmlFor={id} />
      <Input type={type} value={value} placeholder={placeholder} onChange={onChange} disabled={disabled} id={id} />
    </div>
  );
};

export default FormField;