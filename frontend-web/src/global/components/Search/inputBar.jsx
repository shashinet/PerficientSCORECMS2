import React from 'react';
import PropTypes from 'prop-types';

const SearchInput = React.forwardRef(({
  searchLabel,
  placeholderText,
  searchAction,
  clearSearch,
  searchButtonAltText,
  searchButtonText,
}, ref) => {
  const [inputValue, setInputValue] = React.useState(false);

  const handleInputFocus = (e) => {
    if (e.key === 'Escape') {
      setInputValue((prevState) => !prevState);
    }
    if (ref) {
      setInputValue(true);
    } else {
      setInputValue(false);
    }
  };

  const handleKeyFocus = (e) => {
    if (e.key === 'Escape') {
      setInputValue(false);
      clearSearch();
    }

    if (e.key === 'Enter') {
      searchAction();
    }
  };
  return (
    <>
      <label>{searchLabel}</label>
      <div className="input-wrapper">
        <input type="text" ref={ref} placeholder={placeholderText} onChange={handleInputFocus} onKeyUp={handleKeyFocus} />
        <div className="input-search-actions">
          {inputValue ? (
            <button
              type="button"
              onClick={searchAction}
              aria-label={searchButtonAltText}
              className="score-button primary"
            >
              {searchButtonText}
            </button>
          ) : (
            <svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" viewBox="0 0 21 21" fill="none">
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M14.1624 13.1018C15.3101 11.717 16 9.93907 16 8C16 3.58172 12.4183 0 8 0C3.58172 0 0 3.58172 0 8C0 12.4183 3.58172 16 8 16C9.93907 16 11.717 15.3101 13.1018 14.1624L18.9234 19.9841C19.2163 20.277 19.6912 20.277 19.9841 19.9841C20.277 19.6912 20.277 19.2163 19.9841 18.9234L14.1624 13.1018ZM14.5 8C14.5 11.5899 11.5899 14.5 8 14.5C4.41015 14.5 1.5 11.5899 1.5 8C1.5 4.41015 4.41015 1.5 8 1.5C11.5899 1.5 14.5 4.41015 14.5 8Z"
                fill="#222222"
              />
            </svg>
          )}
        </div>
      </div>
    </>
  );
});

SearchInput.propTypes = {
  searchLabel: PropTypes.string.isRequired,
  placeholderText: PropTypes.string.isRequired,
  searchButtonAltText: PropTypes.string.isRequired,
  searchButtonText: PropTypes.string.isRequired,
  searchAction: PropTypes.func.isRequired,
  clearSearch: PropTypes.func.isRequired,
};

export default SearchInput;
