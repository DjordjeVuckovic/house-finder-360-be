import { Outlet } from 'react-router-dom'
import { ReactNode } from 'react'

export const Body = ({ children }: { children: ReactNode }) => {
  return (
    <div style={{ margin: '0 auto' }}>
      {children}
      <Outlet />
    </div>
  )
}
