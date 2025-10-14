import { useNavigate } from 'react-router';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authService } from '../services/authService';

const validationSchema = Yup.object({
  email: Yup.string()
    .required('Email is required')
    .email('Please provide a correct email address!'),
  password: Yup.string()
    .required('Password is required')
    .min(4, 'Password must have at least 4 characters')
});

interface SignInFormValues {
  email: string;
  password: string;
}

const SignIn = () => {
  const navigate = useNavigate();

  const initialValues: SignInFormValues = {
    email: '',
    password: ''
  };

  const handleSubmit = (values: SignInFormValues) => {
    authService.login(values)
      .then((response: any) => {
        localStorage.setItem('token', response.data.accessToken);
        navigate('/home');
      });
  };

  return (
    <div className="container">
      <h1>Sign In</h1>
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleSubmit}
      >
        {({ isValid, dirty }) => (
          <Form>
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
              Sign In
            </button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default SignIn;