using System;

public class bagBall
{
    //  gravitational acceleration.
    public const double G = -9.81;
    double posX, posZ;
    double velX, velZ;
    double timeTotal;

    //  Load the initial position, velocity, and time 
    public bagBall(double x0, double z0, double vx0, double vz0, double time)
	{
        this.posX = x0;
        this.posZ = z0;
        this.velX = vx0;
        this.velZ = vz0;
        this.timeTotal = time;
	}

    //  These methods return the location, velocity, 
    //  and time values
    public double GetVx()
    {
        return this.velX;
    }

    public double GetVz()
    {
        return this.velZ;
    }

    public double GetX()
    {
        return this.posX;
    }

    public double GetZ()
    {
        return this.posZ;
    }

    public double GetTime()
    {
        return this.timeTotal;
    }

    //  This method updates the velocity and position
    //  of the projectile according to the gravity-only model.
    public void UpdateLocationAndVelocity(double dt)
    {
        double x0, z0;
        double vx0, vz0;
        double time0;

        //Get values
        x0 = this.posX;
        z0 = this.posZ;
        vx0 = this.velX;
        vz0 = this.velZ;
        time0 = this.timeTotal;

        //  Update the values
        double time;
        time = time0+dt;

        //  There is no force in the x-direction.
        double vx,x;
        vx = 0;
        x = x0;

        //  The z-location is influenced by the acceleration
        //  due to gravity.
        double vz,z;
        vz = 0;
        z = z0;

        // keep results
        this.posX = x;
        this.posZ = z;
        this.velX = vx;
        this.velZ = vz;
        this.timeTotal = time;
    }
}
