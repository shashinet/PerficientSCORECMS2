/* eslint-disable no-shadow */
/* eslint-disable implicit-arrow-linebreak */
import React from 'react';
import PropTypes from 'prop-types';

// hooks
import useOutsideClick from '../../../hooks/useOutsideClicks';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function NavigationMenuItem(props) {
  const { navItem } = props;
  const { ref, isComponentVisible, setIsComponentVisible } = useOutsideClick(false);

  return (
    <>
      {navItem.childPages.length === 0 ? (
        <li className={['nav-item', `${navItem.selections || ''}`].join('')} role="menuitem">
          <a
            href={navItem.url}
            className={['nav-item-link', navItem.isActive ? 'active' : ''].join(' ')}
            tabIndex="0"
            target={`${navItem.openInNewWindow ? '_blank' : '_self'}`}
            aria-label={navItem.title}
          >
            {navItem.title}
          </a>
        </li>
      ) : (
        <li
          ref={ref}
          role="menuitem"
          className={[
            `${isComponentVisible ? 'nav-item-dropdown open' : 'nav-item-dropdown'}`,
            `${navItem.selections || ''}`,
          ].join(' ')}
        >
          <div className="navitem-wrapper">
            <a
              href={navItem.url}
              className={['menu-item-link', navItem.isActive ? 'active' : ''].join(' ')}
              aria-expanded={isComponentVisible}
              aria-label="Open Toggle sub-navigation"
            >
              <span className="visually-hidden">toggle menu</span>
              <span>{navItem.title}</span>
            </a>
            <span
              role="menuitem"
              className="caret"
              onClick={() => setIsComponentVisible((isComponentVisible) => !isComponentVisible)}
              onKeyDown={() => setIsComponentVisible((isComponentVisible) => !isComponentVisible)}
              tabIndex={0}
            >
              <svg width="14" height="8" viewBox="0 0 14 8" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path
                  d="M1 0.999999L7 7L13 1"
                  stroke="#222222"
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>
            </span>
          </div>
          <ul className="submenu-list" role="menubar">
            {navItem.childPages.map((item, index) => (
              <li className="submenu-list-item" role="menuitem" key={index}>
                <a
                  href={item.url}
                  className={item.isActive ? 'active' : ''}
                  role="menuitem"
                  target={`${item.openInNewWindow ? '_blank' : '_self'}`}
                  aria-label={item.title}
                >
                  {item.title}
                </a>
              </li>
            ))}
          </ul>
        </li>
      )}
    </>
  );
}

NavigationMenuItem.propTypes = {
  navItem: PropTypes.shape({
    selections: PropTypes.string,
    isActive: PropTypes.bool,
    url: PropTypes.string,
    title: PropTypes.string.isRequired,
    openInNewWindow: PropTypes.bool,
    childPages: PropTypes.arrayOf(
      PropTypes.shape({
        title: PropTypes.string,
        url: PropTypes.string,
        openInNewWindow: PropTypes.bool,
        isActive: PropTypes.bool,
      }),
    ),
  }).isRequired,
};
