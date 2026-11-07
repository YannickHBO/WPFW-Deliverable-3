import { useRef, useState } from 'react';
import useDocumentTitle from '../hooks/useDocumentTitle.js';

const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

const fields = [
  {
    name: 'name',
    label: 'Name',
    validate: (value) => {
      const v = value.trim();
      if (!v) return 'Enter your name.';
      if (v.length < 2) return 'Name must be at least 2 characters.';
      if (v.length > 80) return 'Name must be 80 characters or fewer.';
      return '';
    }
  },
  {
    name: 'email',
    label: 'Email',
    validate: (value) => {
      const v = value.trim();
      if (!v) return 'Enter your email address.';
      if (!emailPattern.test(v)) return 'Enter a valid email address.';
      if (v.length > 254) return 'Email must be 254 characters or fewer.';
      return '';
    }
  },
  {
    name: 'message',
    label: 'Message',
    validate: (value) => {
      const v = value.trim();
      if (!v) return 'Enter a feedback message.';
      if (v.length < 2) return 'Message must be at least 2 characters.';
      if (v.length > 1000) return 'Message must be 1000 characters or fewer.';
      return '';
    }
  }
];

const validateAll = (values) =>
  Object.fromEntries(fields.map((field) => [field.name, field.validate(values[field.name])]));

export default function ContactPage() {
  useDocumentTitle('Contact');
  const [values, setValues] = useState({ name: '', email: '', message: '' });
  const [errors, setErrors] = useState({});
  const [attempted, setAttempted] = useState(false);
  const [status, setStatus] = useState({ text: '', isError: false });
  const inputRefs = useRef({});

  const handleChange = (event) => {
    const next = { ...values, [event.target.name]: event.target.value };
    setValues(next);
    if (attempted) {
      setErrors(validateAll(next));
      setStatus({ text: '', isError: false });
    }
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    setAttempted(true);
    const nextErrors = validateAll(values);
    setErrors(nextErrors);

    const firstInvalid = fields.find((field) => nextErrors[field.name]);
    if (firstInvalid) {
      setStatus({ text: 'Please correct the highlighted fields and try again.', isError: true });
      inputRefs.current[firstInvalid.name].focus();
      return;
    }

    setStatus({ text: 'Your message was not sent or saved. This form is a demo only.', isError: false });
  };

  const renderField = ({ name, label }) => {
    const id = `contact-${name}`;
    const error = errors[name];
    const common = {
      id,
      name,
      value: values[name],
      onChange: handleChange,
      ref: (element) => {
        inputRefs.current[name] = element;
      },
      'aria-describedby': `${id}-error`,
      'aria-invalid': attempted ? Boolean(error) : undefined,
      required: true
    };

    return (
      <div className="contact-field" key={name}>
        <label htmlFor={id}>{label}</label>
        {name === 'message' ? (
          <textarea {...common} rows={6} placeholder="Write your feedback here" maxLength={1000} />
        ) : (
          <input
            {...common}
            type={name === 'email' ? 'email' : 'text'}
            autoComplete={name}
            placeholder={name === 'email' ? 'name@example.com' : 'Enter your name'}
            maxLength={name === 'email' ? 254 : 80}
          />
        )}
        <p className="contact-error" id={`${id}-error`} hidden={!error}>
          {error}
        </p>
      </div>
    );
  };

  return (
    <main className="page-content contact-page">
      <h1>Contact Us</h1>
      <p className="contact-intro">Have a question or want to get in touch? Send me a message.</p>

      <form className="contact-form" noValidate onSubmit={handleSubmit}>
        {fields.map(renderField)}
        <p className="contact-note">This form is currently a demo and does not send or store your information.</p>
        <button className="contact-submit" type="submit">
          Send message
        </button>
        <p className={`contact-status${status.isError ? ' is-error' : ''}`} role="status" aria-live="polite">
          {status.text}
        </p>
      </form>
    </main>
  );
}
