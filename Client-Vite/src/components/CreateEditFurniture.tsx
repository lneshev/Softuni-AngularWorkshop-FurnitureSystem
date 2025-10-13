import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import type { Furniture } from '../models/furniture';
import { furnitureService } from '../services/furnitureService';
import styles from './CreateEditFurniture.module.css';

const CreateEditFurniture = () => {
  const [formData, setFormData] = useState<Furniture>({
    id: 0,
    make: '',
    model: '',
    year: 0,
    description: '',
    price: 0,
    image: '',
    material: ''
  });
  const [errors, setErrors] = useState<{[key: string]: string}>({});
  const [touched, setTouched] = useState<{[key: string]: boolean}>({});
  const [loading, setLoading] = useState(false);
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      setLoading(true);
      furnitureService.getFurniture(parseInt(id))
        .then((response) => {
          setFormData(response.data);
          setLoading(false);
        })
        .catch((error) => {
          console.error('Error fetching furniture:', error);
          setLoading(false);
        });
    }
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'year' || name === 'price' ? parseFloat(value) || 0 : value
    }));
  };

  const handleBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    const { name } = e.target;
    setTouched(prev => ({
      ...prev,
      [name]: true
    }));
  };

  const validateForm = () => {
    const newErrors: {[key: string]: string} = {};
    
    if (!formData.make) {
      newErrors.make = 'This field is required';
    } else if (formData.make.length < 4) {
      newErrors.make = 'Length must be at least 4 chars';
    }
    
    if (!formData.model) {
      newErrors.model = 'This field is required';
    } else if (formData.model.length < 4) {
      newErrors.model = 'Length must be at least 4 chars';
    }
    
    if (!formData.year) {
      newErrors.year = 'This field is required';
    } else if (formData.year < 1950) {
      newErrors.year = 'Year must start from 1950';
    } else if (formData.year > 2050) {
      newErrors.year = 'Year must be below 2050';
    }
    
    if (!formData.description) {
      newErrors.description = 'This field is required';
    } else if (formData.description.length < 10) {
      newErrors.description = 'Length must be at least 10 chars';
    }
    
    if (!formData.price) {
      newErrors.price = 'This field is required';
    } else if (formData.price < 0) {
      newErrors.price = 'Min value must be: 0';
    }
    
    if (!formData.image) {
      newErrors.image = 'This field is required';
    }
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (validateForm()) {
      if (id) {
        furnitureService.editFurniture(parseInt(id), formData)
          .then(() => {
            navigate('/furniture/all');
          })
          .catch((error) => {
            console.error('Error editing furniture:', error);
          });
      } else {
        furnitureService.createFurniture(formData)
          .then(() => {
            navigate('/furniture/all');
          })
          .catch((error) => {
            console.error('Error creating furniture:', error);
          });
      }
    }
  };

  if (loading) {
    return (
      <div className="container">
        <div className="text-center">Loading...</div>
      </div>
    );
  }

  const isFormValid = formData.make && formData.model && formData.year && 
                     formData.description && formData.price && formData.image &&
                     Object.keys(errors).length === 0;

  return (
    <div className="container">
      <div className="row space-top">
        <div className="col-md-12">
          <h1>{id ? 'Edit Existing Furniture' : 'Create New Furniture'}</h1>
          <p>Please fill all fields.</p>
        </div>
      </div>
      
      <form onSubmit={handleSubmit}>
        <div className="row space-top">
          <div className="col-md-4">
            <div className="form-group">
              <label className="form-control-label" htmlFor="new-make">Make</label>
              <input 
                className="form-control" 
                id="new-make" 
                type="text" 
                name="make"
                value={formData.make}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {touched.make && errors.make && (
                <div className="alert alert-danger">
                  {errors.make}
                </div>
              )}
            </div>

            <div className="form-group">
              <label className="form-control-label" htmlFor="new-model">Model</label>
              <input 
                className="form-control" 
                id="new-model" 
                type="text" 
                name="model"
                value={formData.model}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {touched.model && errors.model && (
                <div className="alert alert-danger">
                  {errors.model}
                </div>
              )}
            </div>

            <div className="form-group">
              <label className="form-control-label" htmlFor="new-year">Year</label>
              <input 
                className="form-control" 
                id="new-year" 
                type="number" 
                name="year"
                value={formData.year}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {touched.year && errors.year && (
                <div className="alert alert-danger">
                  {errors.year}
                </div>
              )}
            </div>

            <div className="form-group">
              <label className="form-control-label" htmlFor="new-description">Description</label>
              <input 
                className="form-control" 
                id="new-description" 
                type="text" 
                name="description"
                value={formData.description}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {touched.description && errors.description && (
                <div className="alert alert-danger">
                  {errors.description}
                </div>
              )}
            </div>
          </div>
          
          <div className="col-md-4">
            <div className="form-group">
              <label className="form-control-label" htmlFor="new-price">Price</label>
              <input 
                className="form-control" 
                id="new-price" 
                type="number" 
                name="price"
                value={formData.price}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {touched.price && errors.price && (
                <div className="alert alert-danger">
                  {errors.price}
                </div>
              )}
            </div>

            <div className="form-group">
              <label className="form-control-label" htmlFor="new-image">Image</label>
              <input 
                className="form-control" 
                id="new-image" 
                type="text" 
                name="image"
                value={formData.image}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {touched.image && errors.image && (
                <div className="alert alert-danger">
                  {errors.image}
                </div>
              )}
            </div>

            <div className="form-group">
              <label className="form-control-label" htmlFor="new-material">Material (optional)</label>
              <input 
                className="form-control" 
                id="new-material" 
                type="text" 
                name="material"
                value={formData.material}
                onChange={handleChange}
              />
            </div>
            
            <button 
              type="submit" 
              className={`btn btn-primary ${styles.button}`}
              disabled={!isFormValid}
            >
              {id ? 'Edit' : 'Create'}
            </button>
          </div>
        </div>
      </form>
    </div>
  );
};

export default CreateEditFurniture;