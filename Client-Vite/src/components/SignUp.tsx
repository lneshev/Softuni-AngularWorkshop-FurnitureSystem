import { useNavigate } from 'react-router';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authService } from '../services/authService';

const validationSchema = Yup.object({
  name: Yup.string()
    .required('Please provide your name!'),
  email: Yup.string()
    .required('Email is required')
    .email('Please provide a correct email address!'),
  password: Yup.string()
    .required('Password is required')
    .min(4, 'Password must have at least 4 characters!')
});

interface SignUpFormValues {
  name: string;
  email: string;
  password: string;
}

const SignUp = () => {
  const navigate = useNavigate();

  const initialValues: SignUpFormValues = {
    name: '',
    email: '',
    password: ''
  };

  const handleSubmit = (values: SignUpFormValues) => {
    authService.register(values)
      .then(() => {
        navigate('/signin');
      });
  };

  return (
    <div className="container">
      <h1>Sign Up</h1>
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleSubmit}
      >
        {({ isValid, dirty }) => (
          <Form>
            <div className="form-group">
              <label htmlFor="name">Name</label>
              <Field
                type="text"
                className="form-control"
                id="name"
                name="name"
                required
              />
              <ErrorMessage name="name">
                {msg => <div className="alert alert-dark">{msg}</div>}
              </ErrorMessage>
            </div>

            <div className="form-group">
              <label htmlFor="email">Email</label>
              <Field
                type="email"
                className="form-control"
                id="email"
                name="email"
                required
              />
              <ErrorMessage name="email">
                {msg => <div className="alert alert-dark">{msg}</div>}
              </ErrorMessage>
            </div>

            <div className="form-group">
              <label htmlFor="password">Password</label>
              <Field
                type="password"
                className="form-control"
                id="password"
                name="password"
                required
                minLength={4}
              />
              <ErrorMessage name="password">
                {msg => <div className="alert alert-dark">{msg}</div>}
              </ErrorMessage>
            </div>

            <button
              type="submit"
              className="btn btn-dark"
              disabled={!isValid || !dirty}
            >
              Sign Up
            </button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default SignUp;