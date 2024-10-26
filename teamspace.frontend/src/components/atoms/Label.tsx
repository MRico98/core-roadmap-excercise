import React from "react";

interface LabelProps {
  text: string;
  className?: string;
  htmlFor?: string;
}

const Label: React.FC<LabelProps> = ({ text, className, htmlFor }) => {
  return (
    <label className={className} htmlFor={htmlFor}>
      {text}
    </label>
  );
};

export default Label;