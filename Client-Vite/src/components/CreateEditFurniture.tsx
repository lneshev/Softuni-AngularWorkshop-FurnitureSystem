import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import type { Furniture } from '../models/furniture';
import { furnitureService } from '../services/furnitureService';
import styles from './CreateEditFurniture.module.css';

const validationSchema = Yup.object({
  make: Yup.string()
    .required('This field is required')
    .min(4, 'Length must be at least 4 chars'),
  model: Yup.string()
    .required('This field is required')
    .min(4, 'Length must be at least 4 chars'),
  year: Yup.number()
    .required('This field is required')
    .min(1950, 'Year must start from 1950')
    .max(2050, 'Year must be below 2050'),
  description: Yup.string()
    .required('This field is required')
    .min(10, 'Length must be at least 10 chars'),
  price: Yup.number()
    .required('This field is required')
    .min(0, 'Min value must be: 0'),
  image: Yup.string()
    .required('This field is required'),
  material: Yup.string()
});

const CreateEditFurniture = () => {
  const [loading, setLoading] = useState(false);
  const [initialValues, setInitialValues] = useState<Furniture>({
    id: 0,
    make: '',
    model: '',
    year: 0,
    description: '',
    price: 0,
    image: '',
    material: ''
  });
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      setLoading(true);
      furnitureService.getFurniture(parseInt(id))
        .then((response) => {
          setInitialValues(response.data);
          setLoading(false);
        })
        .catch(() => {
          setLoading(false);
        });
    }
  }, [id]);

  const handleSubmit = (values: Furniture) => {
    if (id) {
      furnitureService.editFurniture(parseInt(id), values)
        .then(() => {
          navigate('/furniture/all');
        });
    }
    else {
      furnitureService.createFurniture(values)
        .then(() => {
          navigate('/furniture/all');
        });
    }
  };

  return (
    <div className="container">
      <div className="row space-top">
        <div className="col-md-12">
          <h1>{id ? 'Edit Existing Furniture' : 'Create New Furniture'}</h1>
          {loading ? (
            <div className="text-center">Loading...</div>
          ) : (
            <p>Please fill all fields.</p>
          )}
        </div>
      </div>
      {!loading && (
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={handleSubmit}
          enableReinitialize={true}
        >
          {({ isValid }) => (
            <Form>
              <div className="row space-top">
                <div className="col-md-4">
                  <div className="form-group">
                    <label className="form-control-label" htmlFor="new-make">Make</label>
                    <Field
                      className="form-control"
                      id="new-make"
                      type="text"
                      name="make"
                    />
                    <ErrorMessage name="make">
                      {msg => <div className="alert alert-danger">{msg}</div>}
                    </ErrorMessage>
                  </div>

                  <div className="form-group">
                    <label className="form-control-label" htmlFor="new-model">Model</label>
                    <Field
                      className="form-control"
                      id="new-model"
                      type="text"
                      name="model"
                    />
                    <ErrorMessage name="model">
                      {msg => <div className="alert alert-danger">{msg}</div>}
                    </ErrorMessage>
                  </div>

                  <div className="form-group">
                    <label className="form-control-label" htmlFor="new-year">Year</label>
                    <Field
                      className="form-control"
                      id="new-year"
                      type="number"
                      name="year"
                    />
                    <ErrorMessage name="year">
                      {msg => <div className="alert alert-danger">{msg}</div>}
                    </ErrorMessage>
                  </div>

                  <div className="form-group">
                    <label className="form-control-label" htmlFor="new-description">Description</label>
                    <Field
                      className="form-control"
                      id="new-description"
                      type="text"
                      name="description"
                    />
                    <ErrorMessage name="description">
                      {msg => <div className="alert alert-danger">{msg}</div>}
                    </ErrorMessage>
                  </div>
                </div>

                <div className="col-md-4">
                  <div className="form-group">
                    <label className="form-control-label" htmlFor="new-price">Price</label>
                    <Field
                      className="form-control"
                      id="new-price"
                      type="number"
                      name="price"
                    />
                    <ErrorMessage name="price">
                      {msg => <div className="alert alert-danger">{msg}</div>}
                    </ErrorMessage>
                  </div>

                  <div className="form-group">
                    <label className="form-control-label" htmlFor="new-image">Image</label>
                    <Field
                      className="form-control"
                      id="new-image"
                      type="text"
                      name="image"
                    />
                    <ErrorMessage name="image">
                      {msg => <div className="alert alert-danger">{msg}</div>}
                    </ErrorMessage>
                  </div>

                  <div className="form-group">
                    <label className="form-control-label" htmlFor="new-material">Material (optional)</label>
                    <Field
                      className="form-control"
                      id="new-material"
                      type="text"
                      name="material"
                    />
                  </div>

                  <button
                    type="submit"
                    className={`btn btn-primary ${styles.button}`}
                    disabled={!isValid}
                  >
                    {id ? 'Edit' : 'Create'}
                  </button>
                </div>
              </div>
            </Form>
          )}
        </Formik>
      )}
    </div>
  );
};

export default CreateEditFurniture;